# 🚀 Guia de Deploy

Este documento descreve como fazer o deploy do Sistema de Gestão de Ativos em ambiente de produção.

---

## 📋 Pré-requisitos de Produção

- Servidor Linux (Ubuntu 20.04+ recomendado) ou Windows Server
- .NET 8 Runtime
- MySQL 8.0+
- Nginx ou IIS (para proxy reverso)
- Certificado SSL
- Domínio configurado

---

## 🔧 Preparação do Ambiente

### 1. Servidor Linux (Ubuntu)

#### 1.1. Instalar .NET Runtime

```bash
# Adicionar repositório Microsoft
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Instalar .NET Runtime
sudo apt-get update
sudo apt-get install -y aspnetcore-runtime-8.0
```

#### 1.2. Instalar MySQL

```bash
sudo apt-get install mysql-server
sudo mysql_secure_installation
```

#### 1.3. Instalar Nginx

```bash
sudo apt-get install nginx
```

---

## 📦 Build da Aplicação

### Backend

```bash
# No diretório do projeto
cd GestaoDeAtivosApi

# Publicar aplicação
dotnet publish src/Ativos.Api/Ativos.Api.csproj \
  -c Release \
  -o /var/www/gestao-ativos-api \
  --self-contained false
```

### Frontend

```bash
# No diretório do frontend
cd Gestao-de-Ativos-Inpasa

# Build de produção
npm run build

# Os arquivos estarão em dist/
```

---

## ⚙️ Configuração do Backend

### 1. Criar appsettings.Production.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=projeto_banco_prod;User=app_user;Password=SENHA_SEGURA;"
  },
  "Jwt": {
    "SecretKey": "CHAVE_SUPER_SECRETA_PRODUÇÃO_COM_PELO_MENOS_64_CARACTERES_AQUI",
    "Issuer": "GestaoAtivosApi",
    "Audience": "GestaoAtivosClient",
    "ExpirationMinutes": 120
  },
  "AllowedHosts": "seudominio.com",
  "Cors": {
    "AllowedOrigins": [
      "https://seudominio.com",
      "https://www.seudominio.com"
    ]
  }
}
```

### 2. Criar Usuário MySQL para Aplicação

```sql
CREATE USER 'app_user'@'localhost' IDENTIFIED BY 'SENHA_SEGURA';
CREATE DATABASE projeto_banco_prod CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
GRANT ALL PRIVILEGES ON projeto_banco_prod.* TO 'app_user'@'localhost';
FLUSH PRIVILEGES;
```

### 3. Executar Migrations

```bash
cd /var/www/gestao-ativos-api
dotnet Ativos.Api.dll --environment Production
# Ou usar dotnet ef database update
```

---

## 🔄 Configurar Serviço Systemd

### 1. Criar arquivo de serviço

```bash
sudo nano /etc/systemd/system/gestao-ativos-api.service
```

### 2. Conteúdo do arquivo

```ini
[Unit]
Description=Gestao de Ativos API
After=network.target

[Service]
Type=notify
WorkingDirectory=/var/www/gestao-ativos-api
ExecStart=/usr/bin/dotnet /var/www/gestao-ativos-api/Ativos.Api.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=gestao-ativos-api
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

### 3. Habilitar e iniciar serviço

```bash
sudo systemctl enable gestao-ativos-api.service
sudo systemctl start gestao-ativos-api.service
sudo systemctl status gestao-ativos-api.service
```

---

## 🌐 Configurar Nginx

### 1. Criar configuração do site

```bash
sudo nano /etc/nginx/sites-available/gestao-ativos
```

### 2. Configuração para API

```nginx
server {
    listen 80;
    server_name api.seudominio.com;
    
    location / {
        proxy_pass http://localhost:5234;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }
}
```

### 3. Configuração para Frontend

```nginx
server {
    listen 80;
    server_name seudominio.com www.seudominio.com;
    root /var/www/gestao-ativos-frontend;
    index index.html;

    location / {
        try_files $uri $uri/ /index.html;
    }

    location /api {
        proxy_pass http://localhost:5234;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```

### 4. Habilitar site

```bash
sudo ln -s /etc/nginx/sites-available/gestao-ativos /etc/nginx/sites-enabled/
sudo nginx -t
sudo systemctl reload nginx
```

---

## 🔒 Configurar SSL com Let's Encrypt

### 1. Instalar Certbot

```bash
sudo apt-get install certbot python3-certbot-nginx
```

### 2. Obter certificado

```bash
sudo certbot --nginx -d seudominio.com -d www.seudominio.com -d api.seudominio.com
```

### 3. Renovação automática

```bash
sudo certbot renew --dry-run
```

---

## 📁 Deploy do Frontend

### 1. Copiar arquivos

```bash
# Copiar build para servidor
scp -r dist/* usuario@servidor:/var/www/gestao-ativos-frontend/
```

### 2. Configurar permissões

```bash
sudo chown -R www-data:www-data /var/www/gestao-ativos-frontend
sudo chmod -R 755 /var/www/gestao-ativos-frontend
```

### 3. Atualizar URL da API

No arquivo `index.html` ou configuração do frontend, certifique-se de que a URL da API está correta:

```javascript
const API_BASE_URL = 'https://api.seudominio.com/api';
```

---

## 🔍 Monitoramento

### 1. Logs da Aplicação

```bash
# Ver logs do serviço
sudo journalctl -u gestao-ativos-api.service -f

# Ver logs do Nginx
sudo tail -f /var/log/nginx/access.log
sudo tail -f /var/log/nginx/error.log
```

### 2. Status do Serviço

```bash
sudo systemctl status gestao-ativos-api.service
```

### 3. Uso de Recursos

```bash
# CPU e Memória
htop

# Espaço em disco
df -h

# Conexões MySQL
mysql -u root -p -e "SHOW PROCESSLIST;"
```

---

## 🔄 Atualização da Aplicação

### Script de Deploy Automatizado

```bash
#!/bin/bash
# deploy.sh

echo "🚀 Iniciando deploy..."

# 1. Fazer backup do banco
echo "📦 Backup do banco de dados..."
mysqldump -u app_user -p projeto_banco_prod > backup_$(date +%Y%m%d_%H%M%S).sql

# 2. Parar serviço
echo "⏸️  Parando serviço..."
sudo systemctl stop gestao-ativos-api.service

# 3. Fazer backup da aplicação atual
echo "📦 Backup da aplicação..."
sudo cp -r /var/www/gestao-ativos-api /var/www/gestao-ativos-api.backup

# 4. Build e publicar nova versão
echo "🔨 Build da aplicação..."
cd ~/GestaoDeAtivosApi
git pull
dotnet publish src/Ativos.Api/Ativos.Api.csproj -c Release -o /var/www/gestao-ativos-api

# 5. Executar migrations
echo "🗄️  Executando migrations..."
cd /var/www/gestao-ativos-api
dotnet ef database update

# 6. Reiniciar serviço
echo "▶️  Reiniciando serviço..."
sudo systemctl start gestao-ativos-api.service

# 7. Verificar status
echo "✅ Verificando status..."
sudo systemctl status gestao-ativos-api.service

echo "🎉 Deploy concluído!"
```

---

## 🔐 Segurança em Produção

### Checklist de Segurança

- [ ] Senhas fortes para banco de dados
- [ ] JWT SecretKey com 64+ caracteres aleatórios
- [ ] SSL/TLS configurado (HTTPS)
- [ ] Firewall configurado (UFW ou iptables)
- [ ] Portas desnecessárias fechadas
- [ ] Usuário MySQL específico para aplicação (não root)
- [ ] Logs de acesso habilitados
- [ ] Backup automático configurado
- [ ] Rate limiting no Nginx
- [ ] CORS configurado corretamente
- [ ] Headers de segurança configurados

### Configurar Firewall

```bash
# Permitir apenas portas necessárias
sudo ufw allow 22/tcp   # SSH
sudo ufw allow 80/tcp   # HTTP
sudo ufw allow 443/tcp  # HTTPS
sudo ufw enable
```

### Headers de Segurança no Nginx

```nginx
add_header X-Frame-Options "SAMEORIGIN" always;
add_header X-Content-Type-Options "nosniff" always;
add_header X-XSS-Protection "1; mode=block" always;
add_header Referrer-Policy "no-referrer-when-downgrade" always;
```

---

## 💾 Backup

### Script de Backup Automático

```bash
#!/bin/bash
# backup.sh

BACKUP_DIR="/backups/gestao-ativos"
DATE=$(date +%Y%m%d_%H%M%S)

# Criar diretório se não existir
mkdir -p $BACKUP_DIR

# Backup do banco de dados
mysqldump -u app_user -pSENHA projeto_banco_prod | gzip > $BACKUP_DIR/db_$DATE.sql.gz

# Backup dos arquivos da aplicação
tar -czf $BACKUP_DIR/app_$DATE.tar.gz /var/www/gestao-ativos-api

# Manter apenas últimos 7 dias
find $BACKUP_DIR -name "*.gz" -mtime +7 -delete

echo "Backup concluído: $DATE"
```

### Agendar com Cron

```bash
# Editar crontab
crontab -e

# Adicionar linha para backup diário às 2h da manhã
0 2 * * * /usr/local/bin/backup.sh >> /var/log/backup.log 2>&1
```

---

## 📊 Performance

### Otimizações Recomendadas

1. **Habilitar compressão no Nginx**
```nginx
gzip on;
gzip_types text/plain text/css application/json application/javascript;
```

2. **Cache de arquivos estáticos**
```nginx
location ~* \.(jpg|jpeg|png|gif|ico|css|js)$ {
    expires 1y;
    add_header Cache-Control "public, immutable";
}
```

3. **Connection pooling no MySQL**
```json
"DefaultConnection": "Server=localhost;Database=projeto_banco_prod;User=app_user;Password=SENHA;MaxPoolSize=100;MinPoolSize=5;"
```

---

## 🐛 Troubleshooting em Produção

### Aplicação não inicia

```bash
# Verificar logs
sudo journalctl -u gestao-ativos-api.service -n 50

# Verificar permissões
ls -la /var/www/gestao-ativos-api

# Testar manualmente
cd /var/www/gestao-ativos-api
dotnet Ativos.Api.dll
```

### Erro de conexão com banco

```bash
# Testar conexão
mysql -u app_user -p -h localhost projeto_banco_prod

# Verificar usuário
mysql -u root -p -e "SELECT user, host FROM mysql.user;"
```

### Alto uso de memória

```bash
# Verificar processos
ps aux | grep dotnet

# Limitar memória no systemd
[Service]
MemoryLimit=512M
```

---

## 📞 Suporte

Para problemas em produção:
- Verificar logs: `/var/log/nginx/` e `journalctl`
- Contatar equipe de DevOps
- Abrir ticket de suporte

---

**Última atualização:** Dezembro 2024
