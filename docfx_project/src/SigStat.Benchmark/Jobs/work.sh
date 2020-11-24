maxjobcnt=330

configcnt=$(ls benchmarks -1 | wc -l)
jobcnt=$((configcnt/10))
jobcnt=$((jobcnt<1 ? 1 : jobcnt))
jobcnt=$((jobcnt>maxjobcnt? maxjobcnt : jobcnt))

condor_submit workers.job jobcnt=$jobcnt