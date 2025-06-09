import React, { useState } from 'react';
import type { Announcement } from '../types';
import { getSimilarAnnouncements } from '../api/announcementApi';

interface Props {
  announcement: Announcement; // The announcement to display
  onEdit: (announcement: Announcement) => void; // Callback for editing
  onDelete: (id: string) => Promise<void>; // Callback for deletion
  isDeleting: boolean; // Loading state for delete operation
}

// Component representing a single announcement item
const AnnouncementItem: React.FC<Props> = ({ announcement, onEdit, onDelete, isDeleting }) => {
  const [similar, setSimilar] = useState<Announcement[]>([]); // Similar announcements
  const [isLoadingSimilar, setIsLoadingSimilar] = useState(false); // Loading state for fetching similar announcements
  const [similarError, setSimilarError] = useState<string | null>(null); // Error message if fetching fails

  // Handler to fetch or hide similar announcements
  const fetchSimilar = async () => {
    // If already loaded, toggle them off
    if (similar.length > 0) {
      setSimilar([]);
      return;
    }

    setIsLoadingSimilar(true);
    setSimilarError(null);

    try {
      // Fetch up to 3 similar announcements from the API
      const { data } = await getSimilarAnnouncements(announcement.id, 3);
      setSimilar(data);
    } catch (error) {
      console.error('Failed to fetch similar announcements:', error);
      setSimilarError('Could not load similar announcements.');
    } finally {
      setIsLoadingSimilar(false);
    }
  };

  return (
    <li className="bg-white border border-gray-200 rounded-md shadow-sm p-5 space-y-3 hover:shadow-md transition-shadow">
      {/* Announcement title */}
      <h3 className="text-lg font-semibold text-indigo-600">{announcement.title}</h3>

      {/* Announcement description */}
      <p className="text-gray-700">{announcement.description}</p>

      {/* Action buttons */}
      <div className="flex gap-3 flex-wrap">
        {/* Edit button */}
        <button
          onClick={() => onEdit(announcement)}
          className="bg-yellow-400 hover:bg-yellow-500 text-white px-4 py-1 rounded-md font-semibold shadow-sm transition"
        >
          Edit
        </button>

        {/* Delete button */}
        <button
          onClick={() => onDelete(announcement.id)}
          disabled={isDeleting}
          className="bg-red-500 hover:bg-red-600 text-white px-4 py-1 rounded-md font-semibold shadow-sm disabled:opacity-50 transition"
        >
          {isDeleting ? 'Deleting...' : 'Delete'}
        </button>

        {/* Show/Hide Similar Announcements button */}
        <button
          onClick={fetchSimilar}
          disabled={isLoadingSimilar}
          className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-1 rounded-md font-semibold shadow-sm disabled:opacity-50 transition"
        >
          {isLoadingSimilar ? 'Loading...' : (similar.length > 0 ? 'Hide Similar' : 'Show Similar')}
        </button>
      </div>

      {/* Display error if similar announcements fail to load */}
      {similarError && <p className="text-red-600 text-sm">{similarError}</p>}

      {/* List of similar announcements */}
      {similar.length > 0 && (
        <div className="mt-3 pt-3 border-t border-gray-200">
          <h4 className="text-md font-semibold text-gray-600 mb-1">Similar Announcements:</h4>
          <ul className="list-disc list-inside space-y-1 text-gray-700 text-sm">
            {similar.map(sim => (
              <li key={sim.id}>{sim.title}</li>
            ))}
          </ul>
        </div>
      )}
    </li>
  );
};

export default AnnouncementItem;
