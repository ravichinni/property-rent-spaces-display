import React from 'react';
import PropertyList from './components/PropertyList';
import './App.css';

const App: React.FC = () => {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Property Rent Spaces</h1>
      </header>
      <main>
        <PropertyList />
      </main>
    </div>
  );
};

export default App; 