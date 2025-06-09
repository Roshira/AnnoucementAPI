import { useEffect, useState } from "react";
import { getAllAnnouncements, deleteAnnouncement } from "../api/announcementApi";
import { type Announcement } from "../types";
import AnnouncementForm from "./AnnouncementForm";

export default function AnnouncementList() {
  const [announcements, setAnnouncements] = useState<Announcement[]>([]);
  const [editing, setEditing] = useState<Announcement | null>(null);

  const load = async () => {
    const { data } = await getAllAnnouncements();
    setAnnouncements(data);
    setEditing(null);
  };

  useEffect(() => {
    load();
  }, []);

  return (
    <div className="space-y-4">
      <h1 className="text-2xl font-bold">Announcements</h1>
      <AnnouncementForm onSuccess={load} existing={editing || undefined} />
      <ul className="space-y-2">
        {announcements.map((a) => (
          <li key={a.id} className="border p-4 rounded shadow">
            <h2 className="text-xl font-semibold">{a.title}</h2>
            <p>{a.description}</p>
            <div className="flex gap-2 mt-2">
              <button onClick={() => setEditing(a)} className="bg-yellow-500 text-white px-2 py-1 rounded">Edit</button>
              <button onClick={async () => { await deleteAnnouncement(a.id); load(); }} className="bg-red-500 text-white px-2 py-1 rounded">Delete</button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}