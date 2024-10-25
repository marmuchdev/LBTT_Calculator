import './App.css'
import React from 'react'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import TransactionForm from './TransactionForm'
import NavBar from './NavBar';

const App = () => {
  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/" element={<div className="container mt-5 text-center"><h1>Welcome to LBTTax</h1><p>Your one-stop solution for tax calculations.</p></div>} />
        <Route path="/calculator" element={<TransactionForm />} />
      </Routes>
    </Router>
  );
};

export default App
