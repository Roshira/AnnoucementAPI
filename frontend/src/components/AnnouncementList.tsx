import { useEffect, useState, useCallback } from "react";
import { getAllAnnouncements, deleteAnnouncement as apiDeleteAnnouncement } from "../api/announcementApi";
import type { Announcement } from "../types";
import AnnouncementForm from "./AnnouncementForm";
import AnnouncementItem from "C:/Users/DELL/source/repos/AnnouncementApp/frontend/src/components/AnnouncementItem.tsx";

export default function AnnouncementList() {
  // State to store the list of announcements
  const [announcements, setAnnouncements] = useState<Announcement[]>([]);

  // State for tracking which announcement is being edited
  const [editingAnnouncement, setEditingAnnouncement] = useState<Announcement | undefined>(undefined);

  // Loading state for initial fetch
  const [isLoading, setIsLoading] = useState(true);

  // Error message, if any
  const [error, setError] = useState<string | null>(null);

  // ID of the announcement currently being deleted
  const [deletingId, setDeletingId] = useState<string | null>(null);

  // Function to load announcements from the API
  const loadAnnouncements = useCallback(async (resetEditing = true) => {
    setIsLoading(true);
    setError(null);
    try {
      const { data } = await getAllAnnouncements();
      // Sort announcements alphabetically by title
      setAnnouncements(data.sort((a, b) => a.title.localeCompare(b.title)));
    } catch (err) {
      console.error("Failed to load announcements:", err);
      setError("Failed to load announcements. Please check your connection or try again later.");
    } finally {
      setIsLoading(false);
      if (resetEditing) {
        setEditingAnnouncement(undefined);
      }
    }
  }, []);

  // Load announcements on initial component mount
  useEffect(() => {
    loadAnnouncements();
  }, [loadAnnouncements]);

  // Handler for clicking "Edit"
  const handleEdit = (announcement: Announcement) => {
    setEditingAnnouncement(announcement);
    window.scrollTo({ top: 0, behavior: 'smooth' }); // Smooth scroll to the top where the form is
  };

  // Handler for clicking "Delete"
  const handleDelete = async (id: string) => {
    if (window.confirm('Are you sure you want to delete this announcement?')) {
      setDeletingId(id);
      try {
        await apiDeleteAnnouncement(id);
        await loadAnnouncements(); // Reload list after successful deletion
      } catch (err) {
        console.error('Failed to delete announcement:', err);
        setError('Failed to delete announcement.');
      } finally {
        setDeletingId(null);
      }
    }
  };

  // Called after successful form submission (create or edit)
  const handleFormSuccess = useCallback(async () => {
    await loadAnnouncements();
  }, [loadAnnouncements]);

  // Render loading indicator when loading and list is empty
  if (isLoading && announcements.length === 0) {
    return <div className="text-center py-10 text-gray-500">Loading announcements...</div>;
  }

  // Render error message if there's an error
  if (error) {
    return <div className="text-center py-10 text-red-500 bg-red-100 p-4 rounded-md">{error}</div>;
  }

  return (
    <div className="space-y-6 max-w-3xl mx-auto p-6 bg-gray-50 rounded-lg shadow-md">
      {/* Form for creating or editing an announcement */}
      <AnnouncementForm
        onSuccess={handleFormSuccess}
        existingAnnouncement={editingAnnouncement}
      />

      {/* If there are no announcements */}
      {announcements.length === 0 && !isLoading ? (
        <p className="text-center text-gray-500 italic">No announcements yet. Create one above!</p>
      ) : (
        // Render list of announcement items
        <ul className="space-y-4">
          {announcements.map((a) => (
            <AnnouncementItem
              key={a.id}
              announcement={a}
              onEdit={handleEdit}
              onDelete={handleDelete}
              isDeleting={deletingId === a.id}
            />
          ))}
        </ul>
      )}
    </div>
  );
}
