FROM node:12.18-alpine as build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install
COPY . .
RUN npm run build -- --prod --outputPath=./output


FROM nginx:alpine as nginx

COPY --from=build ./usr/src/app/output /usr/share/nginx/html