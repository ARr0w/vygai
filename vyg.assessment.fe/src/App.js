import logo from './logo.svg';
import React, { useState } from 'react';
import './App.css';
import { Button, Box } from '@mui/material';
import SignUpModal from './SignUpModal';
import SignInModal from './SignInModal';

function App() {

  const [isSignUpModalOpen, setIsSignUpModalOpen] = useState(false);
  const [isSignInModalOpen, setIsSignInModalOpen] = useState(false);

  const handleSignUp = () => setIsSignUpModalOpen(true);
  const handleCloseSignUp = () => setIsSignUpModalOpen(false);

  const handleSignIn = () => setIsSignInModalOpen(true);
  const handleCloseSignIn = () => setIsSignInModalOpen(false);

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          VYG.ai Assessment App
        </p>

        <Box sx={{ display: 'flex', justifyContent: 'center', gap: 2, mt: 2 }}>
          <Button
            onClick={handleSignIn}
            variant="outlined"
            color="primary"
          >
            Sign In
          </Button>
          <Button
            onClick={handleSignUp}
            variant="contained"
            color="secondary"
          >
            Sign Up
          </Button>
        </Box>
      </header>

      <SignUpModal open={isSignUpModalOpen} close={handleCloseSignUp} />
      <SignInModal open={isSignInModalOpen} close={handleCloseSignIn} />
    </div>
  );
}

export default App;
