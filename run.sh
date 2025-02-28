#!/bin/bash
set -e

projectName="userarbolusapi"

docker compose -p $projectName --ansi never kill
docker compose -p $projectName --ansi never down --rmi local --remove-orphans
docker compose -p $projectName --ansi never up -d --build --remove-orphans

