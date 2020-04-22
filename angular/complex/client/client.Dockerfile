### STAGE 1: Build ###
FROM node:12.16.2-alpine3.10 AS build
WORKDIR /app
COPY package.json .
RUN npm install
COPY . .
RUN npm run build 
# STAGE 2: Run ###
FROM nginx
EXPOSE 3000
COPY ./nginx/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/dist/client /usr/share/nginx/html  
