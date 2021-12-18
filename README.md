# Api-Workers-SemaphoreSlim-Mongo
Exemplo de busca de dados em uma API paginada com paralelismo limitando a quantidade de request simultâneos 

```csharp
  public void StartSync()
  {
      CriarLog();

      var totalRequest = this._apiClientService.GetTotalPage();
      if (totalRequest > 0)
      {
          var semaphore = new SemaphoreSlim(5);
          var tasks = new Task[totalRequest];

          for (int i = 0; i < totalRequest; i++)
          {
              var index = i + 1;
              tasks[i] = Task.Run(() =>
              {
                  semaphore.Wait();
                  try
                  {
                      var results = this._apiClientService.GetList(index);
                      foreach (var client in results)
                      {
                          lock (lockObj)
                          {
                              _clientMongoRepository.Save(client);
                          }
                      }
                  }
                  catch (Exception ex)
                  {
                      logger.LogError(ex.Message);
                  }
                  finally
                  {
                      semaphore.Release();
                  }
              });
          }
          Task.WaitAll(tasks);
          CriarLog();
      }
  }
```
