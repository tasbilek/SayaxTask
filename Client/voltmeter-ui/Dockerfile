# Build aşaması
FROM node:20-alpine AS builder
WORKDIR /app
# Paket dosyalarını kopyala ve modülleri yükle
COPY package*.json ./
RUN npm ci

# Uygulama dosyalarını kopyala ve derle
COPY . .
RUN npm run build

# Serve aşaması(nginx)
FROM nginx:alpine
COPY --from=builder /app/dist/voltmeter-ui/browser /usr/share/nginx/html

# Angular için SPA yönlendirmesi
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]