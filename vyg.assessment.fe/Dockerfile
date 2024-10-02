# Use an official Node.js runtime as the base image
FROM node:18

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json to the container
COPY package*.json ./

# Install the project dependencies
RUN npm install

# Copy the rest of the project files to the container
COPY . .

# Build the React app for production
RUN npm run build

# Install a lightweight web server (e.g., serve) to serve the built files
RUN npm install -g serve

# Expose the port that the app will run on
EXPOSE 3000

# Command to serve the app in production mode
CMD ["serve", "-s", "build", "-l", "3000"]