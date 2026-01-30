#!/bin/zsh
grpcurl -plaintext -d '{"menu_id": "1"}' localhost:50051 menu.MenuService/GetMenu