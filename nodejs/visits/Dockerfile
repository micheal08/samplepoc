# Specify a base image
FROM node:12.16.1-alpine3.10

WORKDIR /usr/app

# Run some commands to install additional programs/dependencies
COPY ./package.json ./

RUN npm install

COPY ./ ./

# Specify a command to run on container setup (Default command)

CMD ["npm", "start"]