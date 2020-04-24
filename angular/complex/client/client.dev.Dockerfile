### STAGE 1: Build ###
FROM node:12.16.2-alpine3.10 AS build
WORKDIR /app
COPY package.json .
RUN npm install
COPY . .
CMD ["npm", "run", "start"] 
