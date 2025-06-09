// src/api/announcementApi.ts
import axios from "axios";
import type { Announcement, CreateAnnouncementPayload, EditAnnouncementPayload } from "../types";

const API_URL = "https://localhost:7124/api/announcement"; // Переконайтеся, що URL правильний

export const getAllAnnouncements = () => axios.get<Announcement[]>(API_URL);

export const getAnnouncementById = (id: string) =>
  axios.get<Announcement>(`${API_URL}/${id}`);

export const createAnnouncement = (data: CreateAnnouncementPayload) =>
  axios.post<string>(API_URL, data); // API повертає ID створеного оголошення

export const updateAnnouncement = (data: EditAnnouncementPayload) =>
  axios.put<void>(API_URL, data);

export const deleteAnnouncement = (id: string) =>
  axios.delete<void>(`${API_URL}/${id}`);

export const getSimilarAnnouncements = (id: string, count = 3) =>
  axios.get<Announcement[]>(`${API_URL}/${id}/similar?count=${count}`);