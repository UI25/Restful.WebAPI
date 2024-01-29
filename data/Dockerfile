FROM ubuntu:lastest AS base
WORKDIR /app
COPY ["data/entrypoint.sh","entrypoint.sh"]
COPY ["data/setup.sql","setup.sql"]
RUN chmod +x "entrypoint.sh"
RUN chmod +x "setup.sql"

FROM mcr.microsoft.com/mssql/server:2022-latest AS build
WORKDIR /data

ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=SqlServer123123..

FROM base AS final
WORKDIR /app
ENTRYPOINT [ "/bin/bash", "entrypoint.sh" ]
CMD ["opt/mssql/bin/sqlserver"]