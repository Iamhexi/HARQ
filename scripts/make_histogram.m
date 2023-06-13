data = dlmread ("statistics/retransmissions.csv", ";", 1, 0);
bar(data(:, 1), data(:, 2), 'hist')
grid
xlim([-1 100])
xlabel("Liczba retransmisji")
ylabel("Liczba pakietów")
title("Histogram")
pause