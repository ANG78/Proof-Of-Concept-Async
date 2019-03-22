# ProofOfConcept reagarding to Async/Await

I intent to represent and understand using charts how to work the  keys Async/await in .NET.

The idea is to define several levels of call stack among Async/No Async methods  generating series of data in order to know:

1) how many threads working regarding the number of calls.
2) order of execution of methods involved
3) time elapsed as per cases to be defined by the user
4) what if the method async invocated fihish before or later
5) ...

![alt text](https://github.com/ANG78/ProofOfConcept/edit/master/capture1.png)
