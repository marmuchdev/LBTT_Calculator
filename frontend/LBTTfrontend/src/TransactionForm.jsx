import React, { useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import './TransactionForm.css'; // Import custom styles

const TransactionForm = () => {
  const [transactionDetails, setTransactionDetails] = useState({
    purchasePrice: '',
    ADSamount: 0,
    isFirstTimeBuyers: false,
    isADSEnabled: false,
  });
  const [calculatedTax, setCalculatedTax] = useState(null);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;

    // Ensure non-negative values for purchasePrice and ADSamount
    const validValue = type === 'number' && value < 0 ? 0 : value;

    setTransactionDetails((prev) => {
      const newDetails = { 
        ...prev, 
        [name]: type === 'checkbox' ? checked : validValue 
      };

      if (name === 'isADSEnabled') {
        if (checked) {
          newDetails.isFirstTimeBuyers = false; 
        } else {
          newDetails.ADSamount = 0; 
        }
      }

      return newDetails;
    });
  };

  const submitForm = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post('http://localhost:5069/residential', transactionDetails);
      setCalculatedTax(response.data);
    } catch (error) {
      console.error('Error calculating tax:', error);
      setCalculatedTax(null);
    }
  };

  return (
    <div className="container mt-5">
      <h1 className="text-center">Residential Tax Calculator</h1>
      <div className="row d-flex justify-content-center">
        <div className="col-md-6">
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
                min="0" // Prevent negative input
              />
            </div>
            
            <div className="mb-3 form-check form-switch">
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
                  min="0" // Prevent negative input
                />
              </div>
            )}

            <div className="mb-3 form-check form-switch">
              <input
                name="isFirstTimeBuyers"
                type="checkbox"
                className="form-check-input"
                checked={transactionDetails.isFirstTimeBuyers}
                onChange={handleChange}
                disabled={transactionDetails.isADSEnabled}
              />
              <label className="form-check-label">Is First Time Buyer?</label>
            </div>
            
            <button type="submit" className="btn btn-primary w-100">Calculate Tax</button>
          </form>
        </div>

        {calculatedTax !== null && (
          <div className="col-md-6">
            <div className="result-box mt-4 mt-md-0">
              <h2 className="text-center">Calculated Tax: {calculatedTax}</h2>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default TransactionForm;
