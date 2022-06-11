FROM python:3
FROM gorialis/discord.py

RUN mkdir -p /usr/src/bot
COPY . .

CMD [ "python3", "main.py" ]

