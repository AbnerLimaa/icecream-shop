#!/bin/zsh
grpcurl -plaintext -d '{"menu_id": "1"}' localhost:50052 menu.MenuService/GetMenu