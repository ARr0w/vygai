import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const apiClient = axios.create({
  baseURL: 'https://localhost:32769', 
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('authToken');
    const expiry = new Date(localStorage.getItem('authTokenExpiry'));

    if (token && expiry && new Date() > expiry) {
      localStorage.removeItem('authToken');
      localStorage.removeItem('authTokenExpiry');

      const navigate = useNavigate();
      navigate('/');
    }

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;

  },
  (error) => {
    return Promise.reject(error);
  }
);

apiClient.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response) {
      debugger;
      if (error.response.status === 403 && error.response.data === "Token is blacklisted.") {
        localStorage.removeItem('authToken');

        const navigate = useNavigate();
        navigate('/');
      }
    }
    return Promise.reject(error);
  }
);

const postRequest = async (endpoint, data) => {
  try {
    const response = await apiClient.post(endpoint, data);
    return response.data;
  } catch (error) {
    if (error.status === 400 && error.response && error.response.data && error.response.data.errors) {
        const errors = error.response.data.errors;
        const errorMessages = Object.values(errors).flat();
        throw new Error(errorMessages.join(', '));
      } else {
        console.error('Error fetching user data', error);
        throw error;
      }
  }
}

export const createUser = async (data) => {
   return await postRequest('/api/User/create_user', data);
};

export const login = async (data) => {
  return await postRequest('/api/User/login', data);
};

export const logout = async () => {
  const token = localStorage.getItem('authToken');
  return await postRequest(`/api/User/logout?token=${token}`);
};

export const sendSms = async (data) => {
  return await postRequest('/api/Messaging/sendSms', data);
}
