#!/usr/bin/env zsh
# I am using zsh instead of bash.  I was having some troubles using bash with
# arrays.  Didn't want to investigate, so I just did zsh
WIN_HOME="\/home\/pi\/te\/"
STOW_FOLDERS="workspacer,vscode"
for folder in $(echo $STOW_FOLDERS | sed "s/,/ /g")
do
  filePaths=$(find $folder -type f)

  for sourceFile  in $(echo $filePaths | tr '\n' ' ')
  do 
     targetFullPath=$(echo $sourceFile | sed  "s/^\w*\//$WIN_HOME/g" )
     # create directory
     dirname $sourceFile  | sed  "s/^\w*\//$WIN_HOME/g" | xargs mkdir -p
     cp $sourceFile $targetFullPath
  done
done
