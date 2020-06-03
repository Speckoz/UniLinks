last_commit=$(git rev-parse HEAD)

echo "========= Setting last commit (${last_commit}) ========="
export LAST_COMMIT=${last_commit}

echo "========= Building containers  ========="
docker-compose build

echo "========= Starting containers  ========="
docker-compose up -d

echo "========= Done ========="