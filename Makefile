build:
	mcs -out:bin/main.exe src/*.cs vendor/ReedSolomon/*.cs vendor/CRC/*.cs
run:
	mono bin/main.exe
