MongoDB 2.1.1 for the Raspberry Pi - Work in progress!

Install the needed packages on Raspian:

sudo apt-get install git-core build-essential scons libpcre++-dev xulrunner-dev libboost-dev libboost-program-options-dev libboost-thread-dev libboost-filesystem-dev
Checkout this repo:

git clone git://github.com/RickP/mongopi.git
Build it (this will take very long!):

cd mongopi
scons
Install it:

sudo scons --prefix=/opt/mongo install
This will install mongo in /opt/mongo, to get other programs to see it, you can add this dir to your $PATH:

PATH=$PATH:/opt/mongo/bin/
export PATH


»ò £º  sudo apt-get install MongoDB