import React, { useState } from 'react';
import { Dialog, DialogActions, DialogContent, DialogTitle, TextField, Button } from '@mui/material';
import { createUser } from './apiService';

const SignUpModal = ({ open, close}) => {

    const formDefaultState = {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: '',
    }

    const [formData, setFormData] = useState(formDefaultState);
    const [errors, setErrors] = useState([]);
    const [responseMessage, setResponseMessage] = useState('');

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async () => {
        setResponseMessage('');
        try {
            var response = await createUser(formData);

            if(!response.isCreated){
                setResponseMessage(response.message);
                return;
            }

            handleClose();
        } catch (error) {
            const errorMessages = error.message.split(', ');
            const fieldErrors = {};

            errorMessages.forEach(msg => {
                if (msg.includes('First Name')) {
                    fieldErrors.firstName = msg;
                } else if (msg.includes('Last Name')) {
                    fieldErrors.lastName = msg;
                } else if (msg.includes('Email')) {
                    fieldErrors.email = msg;
                } else if (msg.includes('Password')) {
                    fieldErrors.password = msg;
                    fieldErrors.confirmPassword = msg.replace('Password', 'Confirm Password');
                }
            });

            setErrors(fieldErrors);
        }
    };

    const handleClose = () => {
        setFormData(formDefaultState);
        setResponseMessage('');
        setErrors({});
        close();
    }


    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Sign Up</DialogTitle>
            <DialogContent>
                <TextField
                    name="firstName"
                    label="First Name"
                    value={formData.firstName}
                    onChange={handleInputChange}
                    error={!!errors.firstName}
                    helperText={errors.firstName || ''}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    name="lastName"
                    label="Last Name"
                    value={formData.lastName}
                    onChange={handleInputChange}
                    error={!!errors.lastName}
                    helperText={errors.lastName || ''}
                    fullWidth
                    margin="normal"
                />
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
                <TextField
                    name="confirmPassword"
                    type="password"
                    label="Confirm Password"
                    value={formData.confirmPassword}
                    onChange={handleInputChange}
                    error={!!errors.confirmPassword}
                    helperText={errors.confirmPassword || ''}
                    fullWidth
                    margin="normal"
                />
                <label>{responseMessage}</label>
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
};

export default SignUpModal;