COMMENT=$1

echo "Running GIT BASHHHHHH"
git add --all && git commit -m "$COMMENT"
git push origin master
git pull origin master 