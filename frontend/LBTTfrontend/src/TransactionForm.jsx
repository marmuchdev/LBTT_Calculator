import React, { useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

const TransactionForm = () => {
  const [transactionDetails, setTransactionDetails] = useState({
    purchasePrice: '',
    ADSamount: 0, // Default value
    isFirstTimeBuyers: false,
    isADSEnabled: false, // Toggle for ADS
  });
  const [calculatedTax, setCalculatedTax] = useState(null);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setTransactionDetails((prev) => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value,
    }));
  };

  const submitForm = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post('http://localhost:5069/residential', transactionDetails);
      setCalculatedTax(response.data);
    } catch (error) {
      console.error('Error calculating tax:', error);
      setCalculatedTax(null); // Reset tax on error
    }
  };

  return (
    <div className="container mt-5">
      <h1 className="text-center">Residential Tax Calculator</h1>
      <form onSubmit={submitForm} className="border p-4 rounded shadow">
        <div className="mb-3">
          <label htmlFor="purchasePrice" className="form-label">Purchase Price:</label>
          <input
            name="purchasePrice"
            type="number"
            className="form-control"
            value={transactionDetails.purchasePrice}
            onChange={handleChange}
            required
          />
        </div>
        
        <div className="mb-3 form-check">
          <input
            name="isADSEnabled"
            type="checkbox"
            className="form-check-input"
            checked={transactionDetails.isADSEnabled}
            onChange={handleChange}
          />
          <label className="form-check-label">Include ADS Amount?</label>
        </div>

        {transactionDetails.isADSEnabled && (
          <div className="mb-3">
            <label htmlFor="ADSamount" className="form-label">ADS Amount:</label>
            <input
              name="ADSamount"
              type="number"
              className="form-control"
              value={transactionDetails.ADSamount}
              onChange={handleChange}
              required
            />
          </div>
        )}

        <div className="mb-3 form-check">
          <input
            name="isFirstTimeBuyers"
            type="checkbox"
            className="form-check-input"
            checked={transactionDetails.isFirstTimeBuyers}
            onChange={handleChange}
          />
          <label className="form-check-label">Is First Time Buyer?</label>
        </div>
        
        <button type="submit" className="btn btn-primary">Calculate Tax</button>
      </form>

      {calculatedTax !== null && (
        <div className="mt-4">
          <h2 className="text-center">Calculated Tax: {calculatedTax}</h2>
        </div>
      )}
    </div>
  );
};

export default TransactionForm;
