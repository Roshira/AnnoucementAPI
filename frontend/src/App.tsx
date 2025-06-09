// src/App.tsx
import React from 'react';
import AnnouncementList from './components/AnnouncementList';

const App: React.FC = () => {
  return (
    <div className="min-h-screen bg-gray-100 py-8">
      <header className="text-center mb-8">
        <h1 className="text-4xl font-bold text-indigo-600">Announcements Board</h1>
      </header>
      <main>
        <AnnouncementList />
      </main>
      <footer className="text-center mt-12 text-sm text-gray-500">
        <p>© {new Date().getFullYear()} My Announcement App</p>
      </footer>
    </div>
  );
};

export default App;