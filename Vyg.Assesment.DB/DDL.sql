CREATE TABLE "user" (
    user_id SERIAL PRIMARY KEY,       
    first_name VARCHAR(50) NOT NULL,     
    last_name VARCHAR(50) NOT NULL,    
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,      
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP  
);

CREATE TABLE SmsConfigurations (
    id SERIAL PRIMARY KEY,
    provider_name VARCHAR(50) NOT NULL, 
    key_name VARCHAR(50) NOT NULL,
    key_value VARCHAR(255) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


INSERT INTO SmsConfigurations (provider_name, key_name, key_value)
VALUES 
    ('Twilio', 'AccountSid', 'your_account_sid'),
    ('Twilio', 'AuthToken', 'your_auth_token'),
    ('Twilio', 'PhoneNumber', 'your_twilio_phone_number');


INSERT INTO SmsConfigurations (provider_name, key_name, key_value)
VALUES 
    ('Infobip', 'BaseUrl', 'https://your.infobip.api.url'),
    ('Infobip', 'ApiKey', 'your_infobip_api_key'),
    ('Infobip', 'SenderNumber', 'your_sender_number');