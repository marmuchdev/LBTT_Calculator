import React, { useState } from 'react';
import axios from 'axios';

const TransactionForm = () => {
  const [transactionDetails, setTransactionDetails] = useState({
    purchasePrice: '',
    ADSamount: '',
    isFirstTimeBuyers: false,
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
    <div>
      <h1>Residential Tax Calculator</h1>
      <form onSubmit={submitForm}>
        <div>
          <label htmlFor="purchasePrice">Purchase Price:</label>
          <input
            name="purchasePrice"
            type="number"
            value={transactionDetails.purchasePrice}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label htmlFor="ADSamount">ADS Amount:</label>
          <input
            name="ADSamount"
            type="number"
            value={transactionDetails.ADSamount}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label>
            <input
              name="isFirstTimeBuyers"
              type="checkbox"
              checked={transactionDetails.isFirstTimeBuyers}
              onChange={handleChange}
            />
            Is First Time Buyer?
          </label>
        </div>
        <button type="submit">Calculate Tax</button>
      </form>

      {calculatedTax !== null && (
        <div>
          <h2>Calculated Tax: {calculatedTax}</h2>
        </div>
      )}
    </div>
  );
};

export default TransactionForm;
