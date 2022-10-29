# ---------- BUILD ----------------
FROM node:latest AS ng-builder
RUN mkdir -p /app
WORKDIR /app

# Copio o package.json para a pasta que ficará a aplicação
# para conseguir instalar todas as dependências
COPY package.json /app
RUN npm install --force

# Agora copia tudo o que estiver no diretório do dockerfile
# para a pasta app dentro do container e faço o build 
# para gerar os arquivos e a pasta /app/dist/web-app
COPY . /app
RUN $(npm bin)/ng build --configuration production


# ----------- NGINX --------------
# Agora subo um novo container rodando o nginx
# que irá servir a app angular
FROM nginx
COPY nginx.prod.conf /etc/nginx/nginx.prod.conf

# Copio os arquivos do step anterior de build para o container/pasta que o nginx irá servir a webapp
COPY --from=ng-builder /app/dist/web-app /usr/share/nginx/html

EXPOSE 80