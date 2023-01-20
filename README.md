This program will give you a whole new way of customizing your personal computer by changing the background of your Desktop!

It will download one of the images of top posts of the subreddit earthporn and will set it as wallpaper.

Run > shell:startup > add shortcut to program

wget -O - -q reddit.com/r/earthporn.json | jq '.data.children[] |.data.url' | head -1 |