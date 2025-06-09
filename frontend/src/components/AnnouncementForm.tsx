import React, { useState, useEffect } from 'react';
import type { Announcement } from '../types';

interface Props {
  onSubmit?: (announcement: Omit<Announcement, 'id'>) => void;
  onSuccess: () => Promise<void>;
  existing?: Announcement;
}

const AnnouncementForm: React.FC<Props> = ({ onSuccess, existing }) => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');

  useEffect(() => {
    if (existing) {
      setTitle(existing.title);
      setDescription(existing.description);
    } else {
      setTitle('');
      setDescription('');
    }
  }, [existing]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const payload = { title, description };

    if (existing) {
      await fetch(`https://localhost:7124/api/announcement/${existing.id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
    } else {
      await fetch('https://localhost:7124/api/announcement', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
    }

    setTitle('');
    setDescription('');
    await onSuccess();
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-2">
      <input
        value={title}
        onChange={e => setTitle(e.target.value)}
        placeholder="Title"
        className="border p-2 w-full"
      />
      <textarea
        value={description}
        onChange={e => setDescription(e.target.value)}
        placeholder="Description"
        className="border p-2 w-full"
      />
      <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded">
        {existing ? 'Update' : 'Create'}
      </button>
    </form>
  );
};

export default AnnouncementForm;
