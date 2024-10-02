import React, { useState, useEffect } from 'react';
import './dashboard.css'
import { useNavigate } from 'react-router-dom';
import { TextField, Button } from '@mui/material';
import {sendSms, logout} from './apiService'

const Dashboard = () => {

    const navigate = useNavigate();

    useEffect(() => {
        const expiry = localStorage.getItem('authTokenExpiry');
        const expiryDate = new Date(expiry);

        if (!expiry || new Date() > expiryDate) {
            navigate('/');
        }
    }, [navigate]);

    const firstName = localStorage.getItem("firstName");
    const lastName = localStorage.getItem("lastName");

    const defaultFormState = {
        providerName : '',
        receipientPhoneNumber: '',
        message: ''
    }

    const [formData, setFormData] = useState(defaultFormState);
    const [errors, setErrors] = useState([]);
    const [responseMessage, setResponseMessage] = useState('');
    
    const [responseMessageStyle, setResponseMessageStyle] = useState({
        color: 'red'
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    }

    const sendMessage = async () => {
        setResponseMessageStyle({color: 'red'});
        try{
            if(isFormValid()){
                var response = await sendSms(formData);
                setResponseMessage(response.message);

                if(response.message.indexOf('sent') >= 0){
                    setResponseMessageStyle({color: 'green'});
                }
                return;
            }
        }
        catch(error){
        }
    }

    const isFormValid = () => {
        setResponseMessage('');

        const fieldErrors = {
            providerName: '',
            receipientPhoneNumber: '',
            message: ''
        };
        
        const phoneNumberRegex = /^\+?[1-9]\d{1,14}$/;

        if(formData.providerName.trim() === ''){
            fieldErrors.providerName = "Provider name is required."
        }

        if(formData.receipientPhoneNumber.trim() === ''){
            fieldErrors.receipientPhoneNumber = "Receipient Phone Number is required."
        }
        else{
            if (!phoneNumberRegex.test(formData.receipientPhoneNumber)) {
                fieldErrors.receipientPhoneNumber = "Invalid phone number."
            }
        }

        if(formData.message.trim() === ''){
            fieldErrors.message = "Message is required."
        }

        setErrors(fieldErrors);

        return formData.providerName.trim() !== ''
        && formData.receipientPhoneNumber.trim() !== ''
        && phoneNumberRegex.test(formData.receipientPhoneNumber)
        && formData.message.trim() !== ''
    }

    const handleLogout = async () => {
        
        await logout();

        localStorage.removeItem('authToken');
        localStorage.removeItem('authTokenExpiry');
        localStorage.removeItem('firstName');
        localStorage.removeItem('lastName');
        
        navigate('/');
    };

    return (
        <div className="center-container">
            <header>
            <div className="header-container">
                    <p>Welcome {firstName} {lastName}</p>
                    <Button
                        className="logout-button"
                        color="secondary"
                        variant="contained"
                        onClick={handleLogout}
                    >
                        Logout
                    </Button>
                </div>
            </header>
            <div className='form'>
                <TextField
                    name="providerName"
                    label="Message Provider"
                    value={formData.providerName}
                    onChange={handleInputChange}
                    error={!!errors.providerName}
                    helperText={errors.providerName || ''}
                    fullWidth
                    margin="normal"
                    className="text-field"
                />
                <TextField
                    name="receipientPhoneNumber"
                    label="Receipient Phone Number"
                    value={formData.receipientPhoneNumber}
                    onChange={handleInputChange}
                    error={!!errors.receipientPhoneNumber}
                    helperText={errors.receipientPhoneNumber || ''}
                    fullWidth
                    margin="normal"
                    className="text-field"
                />
                <TextField
                    name="message"
                    label="Message"
                    value={formData.message}
                    onChange={handleInputChange}
                    error={!!errors.message}
                    helperText={errors.message || ''}
                    multiline
                    rows={4}
                    fullWidth
                    margin="normal"
                    className="text-field"
                />

                <label className="response-message" style={responseMessageStyle}>{responseMessage}</label>

                <Button color="primary" variant="contained" margin="normal" onClick={sendMessage}>
                    Send
                </Button>
            </div>
        </div>
    )
}

export default Dashboard;