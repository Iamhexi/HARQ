data = dlmread ("statistics/info.csv", ";", 2, 0);
bar(data(:, 1), data(:, 2), 'hist')
grid
xlabel("Liczba retransmisji")
ylabel("Liczba pakietów")
title("Histogram")
pause