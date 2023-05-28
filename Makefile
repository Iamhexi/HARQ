build:
	mcs -out:bin/main.exe src/*.cs src/vendor/ReedSolomon/*.cs
run:
	mono bin/main.exe
