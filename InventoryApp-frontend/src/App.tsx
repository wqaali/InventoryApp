import React from 'react';
import logo from './logo.svg';
import './App.css';
import StockGrid from './Components/StockGrid';

function App() {
  return (
    <div style={{ padding: 20 }}>

      <h2>Live Inventory Dashboard</h2>

      <StockGrid />

    </div>
  );
}

export default App;
