﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MessageMQ.Adapter
{
    public interface IMQAdapter<T> where T : class
    {

        /// <summary>
        /// Gets or sets the queu e_ destination.
        /// </summary>
        /// <value>
        /// The queu e_ destination.
        /// </value>
        string QUEUE_DESTINATION { get; set; }

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        int SendMessage<T>(T t);


        /// <summary>
        /// Sends the messages.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        int SendMessages<T>(T[] t);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns></returns>
        int SendMessage<T>(T t, string queueName);

        int SendMessage<T>(T t, string queueName, string selector = "");
        /// <summary>
        /// Sends the messages.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <param name="queueName">Name of the queue.</param>
        /// <returns></returns>
        int SendMessages<T>(T[] t, string queueName);

        /// <summary>
        /// The recevice message.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T[]"/>.
        /// </returns>
        T[] ReceviceMessage<T>() where T : class;

        /// <summary>
        /// The recevice listener.
        /// </summary>
        void ReceviceListener<T>() where T : class;

        /// <summary>
        /// Occurs when [mq listener].
        /// </summary>
        event MQMessageListener<T> MQListener;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        bool IsConnected { get; set; }
    }

    /// <summary>
    /// MessageListener
    /// </summary>
    /// <param name="status">The status.</param>
    public delegate void MQMessageListener<T>(T status);
}
