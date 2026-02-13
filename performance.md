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
| LCh CIEDE2000| 758 ms |
| OK | 789 ms |
| Z | 851 ms |
| CAM16 | 2.74 s |
| CAM02 | 3.55 s |
