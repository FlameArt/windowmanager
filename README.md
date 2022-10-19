# Command line window manager for Windows

Move, resize, show window by coordinates

## Usage

Place **VSCode** and **Chrome** from left to right:

    wm.exe title "Visual studio code" x 0 y 0 w 1153 h 1041
    wm.exe name "chrome" x 1145 y 0 w 759 h 1041

## Arguments

* `-name` find window by part of process name (case insensitive)
* `-title` find window by part of title (case insensitive)
* `-pid` find window by pid of process
* `-x` pos x
* `-y` pos y
* `-h` height
* `-w` width
