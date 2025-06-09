export interface Announcement {
  id: string; // GUID буде представлений як string у JSON
  title: string;
  description: string;
}

export interface CreateAnnouncementPayload {
  title: string;
  description: string;
}

export interface EditAnnouncementPayload extends CreateAnnouncementPayload {
  id: string;
}