# Use Node.js for Angular build
FROM node:18 as build
WORKDIR /app

# Copy package.json & install dependencies
COPY package.json package-lock.json ./
RUN npm install

# Copy app files & build
COPY . .
RUN npm run build --prod

# Use Nginx to serve Angular app
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
