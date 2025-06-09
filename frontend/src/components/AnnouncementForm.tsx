// Import React and necessary hooks
import React, { useState, useEffect } from 'react';
import type { Announcement, CreateAnnouncementPayload, EditAnnouncementPayload } from '../types';
import { createAnnouncement, updateAnnouncement } from '../api/announcementApi';

interface Props {
  onSuccess: () => Promise<void> | void;
  existingAnnouncement?: Announcement; // Optional prop for editing an existing announcement
}

// Component for both creating and editing announcements
const AnnouncementForm: React.FC<Props> = ({ onSuccess, existingAnnouncement }) => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Fill form fields if editing an existing announcement
  useEffect(() => {
    if (existingAnnouncement) {
      setTitle(existingAnnouncement.title);
      setDescription(existingAnnouncement.description);
    } else {
      setTitle('');
      setDescription('');
    }
  }, [existingAnnouncement]);

  // Form submission handler
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    setError(null);

    // Basic validation
    if (!title.trim() || !description.trim()) {
      setError('Title and description are required.');
      setIsLoading(false);
      return;
    }

    try {
      if (existingAnnouncement) {
        // Prepare payload for editing
        const payload: EditAnnouncementPayload = {
          id: existingAnnouncement.id,
          title,
          description,
        };
        // Send update request
        await updateAnnouncement({ id: payload.id, title: payload.title, description: payload.description });
      } else {
        // Prepare payload for creating
        const payload: CreateAnnouncementPayload = { title, description };
        await createAnnouncement(payload);
      }
      // Clear form fields
      setTitle('');
      setDescription('');
      await onSuccess(); // Notify parent component
    } catch (err) {
      console.error('Failed to save announcement:', err);
      setError('Failed to save announcement. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="space-y-4 p-6 bg-white rounded-md shadow-md border border-gray-200"
    >
      {/* Form title changes depending on mode */}
      <h2 className="text-2xl font-semibold text-gray-800">
        {existingAnnouncement ? 'Edit Announcement' : 'Create New Announcement'}
      </h2>

      {/* Error message display */}
      {error && (
        <p className="text-red-600 bg-red-100 p-2 rounded-md border border-red-300">{error}</p>
      )}

      {/* Title input */}
      <div>
        <label
          htmlFor="title"
          className="block mb-1 font-medium text-gray-700"
        >
          Title
        </label>
        <input
          id="title"
          type="text"
          value={title}
          onChange={e => setTitle(e.target.value)}
          placeholder="Enter announcement title"
          disabled={isLoading}
          className="w-full p-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-400 focus:border-indigo-500"
        />
      </div>

      {/* Description input */}
      <div>
        <label
          htmlFor="description"
          className="block mb-1 font-medium text-gray-700"
        >
          Description
        </label>
        <textarea
          id="description"
          value={description}
          onChange={e => setDescription(e.target.value)}
          placeholder="Enter announcement description"
          rows={4}
          disabled={isLoading}
          className="w-full p-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-400 focus:border-indigo-500"
        />
      </div>

      {/* Action buttons */}
      <div className="flex items-center gap-3">
        <button
          type="submit"
          disabled={isLoading}
          className="bg-indigo-600 hover:bg-indigo-700 text-white font-semibold px-5 py-2 rounded-md shadow transition"
        >
          {isLoading ? 'Saving...' : existingAnnouncement ? 'Update' : 'Create'}
        </button>

        {/* Cancel button appears only in edit mode */}
        {existingAnnouncement && (
          <button
            type="button"
            onClick={() => {
              setTitle('');
              setDescription('');
              onSuccess(); // Return to previous view or reset state
            }}
            disabled={isLoading}
            className="bg-gray-300 hover:bg-gray-400 text-gray-700 font-semibold px-4 py-2 rounded-md shadow transition"
          >
            Cancel
          </button>
        )}
      </div>
    </form>
  );
};

export default AnnouncementForm;
