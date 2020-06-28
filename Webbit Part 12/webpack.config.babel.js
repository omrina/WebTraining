import {HotModuleReplacementPlugin} from 'webpack';
import HtmlWebpackPlugin from 'html-webpack-plugin';
import path from 'path';

export default {
  entry: {
    main: './client/app.js'
  },
  output: {
    path: path.resolve(__dirname, `dist`),
    filename: '[name].[hash].js'
  },
  module: {
    rules: [
      {
        test: /\.js$/,
        use: ['babel-loader'],
        exclude: /node_modules/
      },
      {
        test: /\.html$/,
        use: ['html-loader']
      },
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
      },
      {
        test: /\.(jpe?g|png|gif|svg|eot|woff2?|ttf)(\?.*)?$/i,
        use: [{
          loader: 'file-loader',
          options: {
            name: '/[hash].[ext]'
          }
        }]
      },
      {
        test: /\.less$/,
        use: ['style-loader', 'css-loader', 'less-loader']
      }
    ]
  },
  plugins: [
    new HotModuleReplacementPlugin(),
    new HtmlWebpackPlugin({
      template: `./client/index.html`,
      inject: 'body'
    })
  ],
  devtool: 'sourcemap',
  devServer: {
    inline: true,
    historyApiFallback: true,
    hot: true,
    port: 8000,
    proxy: {
      '/auth': {
        target: 'http://localhost:8080/'
      },
      '/api': {
        target: 'http://localhost:8080/'
      }
    }
  }
};