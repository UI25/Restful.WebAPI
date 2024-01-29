#!/bin/bash
set -e
if["$1"='/opt/mssql/bin/sqlserver'];then
    if[! -f /tmp/app-initalized];then
        function initialize_app_database(){
            sleep 15s
            /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Sygzalp09102016.. -d master -i setup.sql
            touch /tmp/app-initalized
        }
        initialize_app_database &
    fi
fi

exec "$@"
