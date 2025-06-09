import axios from "axios";

const API_URL = "https://localhost:7124/api/announcement";

export const getAllAnnouncements = () => axios.get(API_URL);
export const getAnnouncementById = (id: string) => axios.get(`${API_URL}/${id}`);
export const createAnnouncement = (data: { title: string; description: string }) => axios.post(API_URL, data);
export const updateAnnouncement = (data: { id: string; title: string; description: string }) => axios.put(API_URL, data);
export const deleteAnnouncement = (id: string) => axios.delete(`${API_URL}/${id}`);
export const getSimilarAnnouncements = (id: string, count = 3) => axios.get(`${API_URL}/${id}/similar?count=${count}`);
