# Performance Test Results

This document is for capturing performance results across different versions of the software over time.

The initial performance tests are rather informal, as they capture execution time for a specifically defined test, which is bound to be a machine-specific metric. But with the intention to run the tests on the same machine, at least it should give some decent relative results. Tests are also run 10 times per algorithm to get an average to account for the degree of randomization in the tests.

## v1.0

| Algorithm | Execution Time |
| --------- | -------------- |
| RGB Euclidean | 73 ms |
| RGB Redmean | 73 ms |
| Lab Hybrid | 404 ms |
| Lab CIE94 | 571 ms |
| Lab CIE76 | 587 ms |
| CMC Perceptibility | 589 ms |
| CMC Acceptability | 595 ms |
| ITP | 740 ms |
| LCh CIEDE2000 | 758 ms |
| OK | 789 ms |
| Z | 851 ms |
| CAM16 | 2.74 s |
| CAM02 | 3.55 s |

## v1.0.1

The original "slow" mode experienced some apparent boosts from dependency updates.

| Algorithm | Execution Time |
| --------- | -------------- |
| RGB Redmean | 71 ms |
| RGB Euclidean | 73 ms |
| Lab Hybrid | 380 ms |
| CMC Perceptibility | 508 ms |
| Lab CIE94 | 517 ms |
| CMC Acceptability | 527 ms |
| Lab CIE76 | 544 ms |
| LCh CIEDE2000 | 685 ms |
| ITP | 705 ms |
| OK | 782 ms |
| Z | 804 ms |
| CAM16 | 2.11 s |
| CAM02 | 2.74 s |

This version also added "fast" mode, with results below (RGB Euclidean and Redmean are excluded as they are already fast enough to not experience a benefit from fast mode):

| Algorithm | Execution Time (ms) |
| --------- | -------------- |
| CMC Perceptibility | 198 |
| OK | 202 |
| Z | 252 |
| CMC Acceptability | 257 |
| ITP | 262 |
| CAM16 | 310 |
| Lab Hybrid | 311 |
| Lab CIE94 | 338 |
| LCh CIEDE2000 | 394 |
| CAM02 | 564 |
| Lab CIE76 | 619 |
