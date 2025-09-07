import requests
import json
from typing import Optional, Dict, Any, Union
from uuid import UUID

class UserServiceClient:
    """
    Клиент для взаимодействия с микросервисом пользователей на C#
    """

    def __init__(self, base_url: str = "http://localhost:5233"):
        """
        Инициализация клиента
        
        :param base_url: Базовый URL микросервиса
        """
        self.base_url = base_url.rstrip('/')
        self.token = None
        self.session = requests.Session()

    def _handle_response(self, response: requests.Response) -> Optional[Union[dict, list, str, UUID]]:
        """
        Обработка ответа от сервера
        
        :param response: Ответ requests
        :return: Распарсенные данные или None при ошибке
        """
        try:
            if response.status_code == 200:
                # Проверяем, есть ли содержимое в ответе
                if response.content:
                    try:
                        return response.json()
                    except json.JSONDecodeError:
                        # Если не JSON, возвращаем текст
                        return response.text
                return None
            elif response.status_code == 401:
                print("Unauthorized. Try to login first.")
            elif response.status_code == 404:
                print("Resource not found.")
            else:
                print(f"Request failed: {response.status_code} - {response.text}")
            return None
        except Exception as e:
            print(f"Response handling error: {e}")
            return None

    def login(self) -> Optional[str]:
        """
        Получение JWT токена для аутентификации
        
        :return: JWT токен или None при ошибке
        """
        try:
            url = f"{self.base_url}/api/clients"
            response = self.session.post(url)

            result = self._handle_response(response)
            if result and isinstance(result, dict):
                self.token = result.get('access_token')

                # Обновляем заголовки сессии
                if self.token:
                    self.session.headers.update({
                        'Authorization': f'Bearer {self.token}',
                        'Content-Type': 'application/json'
                    })

                return self.token
            return None

        except Exception as e:
            print(f"Login error: {e}")
            return None

    def create_user(self,
                    external_id: str,
                    name: str,
                    password: str,
                    created_by: str) -> Optional[str]:
        """
        Создание нового пользователя
        
        :param external_id: Внешний идентификатор пользователя
        :param name: Имя пользователя
        :param password: Пароль
        :param created_by: Кто создал пользователя
        :return: ID созданного пользователя или None при ошибке
        """
        try:
            url = f"{self.base_url}/api/users"

            payload = {
                "externalId": external_id,
                "name": name,
                "password": password,
                "createdBy": created_by
            }

            response = self.session.post(url, json=payload)
            return self._handle_response(response)

        except Exception as e:
            print(f"Create user error: {e}")
            return None

    def authorize_user(self, username: str, password: str) -> bool:
        """
        Авторизация пользователя
        
        :param username: Имя пользователя
        :param password: Пароль
        :return: True если авторизация успешна, False в противном случае
        """
        try:
            url = f"{self.base_url}/api/users"

            params = {
                "username": username,
                "password": password
            }

            response = self.session.get(url, params=params)
            return response.status_code == 200

        except Exception as e:
            print(f"Authorize error: {e}")
            return False

    def set_token(self, token: str) -> None:
        """
        Установка токена вручную
        
        :param token: JWT токен
        """
        self.token = token
        self.session.headers.update({
            'Authorization': f'Bearer {token}',
            'Content-Type': 'application/json'
        })

    def clear_token(self) -> None:
        """
        Очистка токена
        """
        self.token = None
        if 'Authorization' in self.session.headers:
            del self.session.headers['Authorization']

    def get_session_info(self) -> Dict[str, Any]:
        """
        Получение информации о текущей сессии
        
        :return: Информация о сессии
        """
        return {
            'base_url': self.base_url,
            'has_token': self.token is not None,
            'headers': dict(self.session.headers)
        }