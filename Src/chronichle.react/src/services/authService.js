import axios from 'axios';

const API_BASE_URL = "https://localhost:5000/api/Identity";

export const register = async (data) => {
    const response = await axios.post(`${API_BASE_URL}/Register`, data);
    return response.data;
};

export const login = async (data) => {
    const response = await axios.post(`${API_BASE_URL}/Login`, data);
    return response.data;
};
