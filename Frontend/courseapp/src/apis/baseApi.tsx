import httpCodeStatuse from "../utilitys/httpCodeStatus";
import dataType from "../utilitys/dataTypeForTransmit";

//basic fetch get request, return an instance of promise
const Get = (url: string, headers: HeadersInit, dataTypeRes: number) => {
  return new Promise((resolve, reject) => {
    fetch(url, {
      method: "get",
      mode: "cors",
      headers: headers,
    })
      .then((response) => {
        if (response.status != httpCodeStatuse.HttpSuccessCode) {
          throw new Error(response.status.toString());
        }

        //return different type of request data jason or text
        switch (dataTypeRes) {
          case dataType.dataJson: {
            resolve(response.json());
            break;
          }
          case dataType.dataText: {
            resolve(response.text());
            break;
          }
        }
      })
      .catch((error) => {
        reject(error);
      });
  });
};

const Post = (url: string, headers: HeadersInit, body: any) => {
  return new Promise((resolve, reject) => {
    fetch(url, {
      method: "post",
      mode: "cors",
      headers: headers,
      body: JSON.stringify(body),
    })
      .then((response) => {
        if (response.status != httpCodeStatuse.HttpSuccessCode) {
          throw new Error(response.status.toString());
        }
        resolve(response);
      })
      .catch((error) => {
        reject(error);
      });
  });
};

const PostWithData = (
  url: string,
  headers: HeadersInit,
  body: any,
  dataTypeRes: number
) => {
  return new Promise((resolve, reject) => {
    fetch(url, {
      method: "post",
      mode: "cors",
      headers: headers,
      body: JSON.stringify(body),
    })
      .then((response) => {
        if (response.status !== httpCodeStatuse.HttpSuccessCode) {
          throw new Error(response.status.toString());
        }
        switch (dataTypeRes) {
          case dataType.dataJson: {
            resolve(response.json());
            break;
          }
          case dataType.dataText: {
            resolve(response.text());
            break;
          }
          default:
            break;
        }
      })
      .catch((error) => {
        reject(error);
      });
  });
};

const Put = (url: string, headers: HeadersInit, body: BodyInit) => {
  return new Promise((resolve, reject) => {
      fetch(url,
          {
              method: 'put',
              mode: 'cors',
              headers: headers,
              body: JSON.stringify(body)
          }).then(response => {
              if (response.status !== httpCodeStatuse.HttpSuccessCode) {
                  throw new Error(response.status.toString());
              }
              resolve(response);
          }).catch((error) => {
              reject(error);
          })
  });
};

export { Get, Post,PostWithData };
