this gets 

wget -O - -q reddit.com/r/earthporn.json | jq '.data.children[] |.data.url' | head -1 |