import React, { useState } from 'react';
import { Dialog, DialogActions, DialogContent, DialogTitle, TextField, Button } from '@mui/material';
import { login } from './apiService';
import { useNavigate } from 'react-router-dom';

const SignInModal = ({ open, close }) => {
    const formDefaultState = {
        email: '',
        password: ''
    }

    const [formData, setFormData] = useState(formDefaultState);
    const [errors, setErrors] = useState([]);
    const [responseMessage, setResponseMessage] = useState('');

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const navigate = useNavigate();

    const handleSubmit = async () => {
        try {
            let result = await login(formData);

            if(result.message.trim() !== ''){
                setResponseMessage(result.message);
                return;
            }

            localStorage.setItem('authToken', result.tokenDetails.token);
            localStorage.setItem('authTokenExpiry', result.tokenDetails.expiry);
            localStorage.setItem('firstName', result.tokenDetails.firstName);
            localStorage.setItem('lastName', result.tokenDetails.lastName);

            navigate('/dashboard');

            handleClose();
        } catch (error) {
            const errorMessages = error.message.split(', ');
            const fieldErrors = {};

            errorMessages.forEach(msg => {
                if (msg.includes('Email')) {
                    fieldErrors.email = msg;
                } else if (msg.includes('Password')) {
                    fieldErrors.password = msg;
                }
            });

            setErrors(fieldErrors);
        }
    };

    const handleClose = () => {
        setFormData(formDefaultState);
        setErrors({});
        close();
    }

    return (
        <Dialog open={open} onClose={handleClose} >
            <DialogTitle>Sign In</DialogTitle>
            <DialogContent>
                <label>{responseMessage}</label>
                <TextField
                    name="email"
                    label="Email"
                    value={formData.email}
                    onChange={handleInputChange}
                    error={!!errors.email}
                    helperText={errors.email || ''}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    name="password"
                    type="password"
                    label="Password"
                    value={formData.password}
                    onChange={handleInputChange}
                    error={!!errors.password}
                    helperText={errors.password || ''}
                    fullWidth
                    margin="normal"
                />
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose} color="primary">
                    Cancel
                </Button>
                <Button onClick={handleSubmit} color="primary" variant="contained">
                    Submit
                </Button>
            </DialogActions>
        </Dialog>
    );
}

export default SignInModal